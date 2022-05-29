import React, { useState } from 'react';
import { TextField } from '@mui/material';
import { useForm } from 'react-hook-form';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { LoadingButton } from '@mui/lab';
import { BasicCard } from '../../../components/basic-card';
import './styles.css';
import { SignInRequest } from './requests';
import { api } from '../../../services/api';
import { useAppDispatch } from '../../../store/hooks';
import { toastActions } from '../../toast/toastSlice';
import { LoginResponse } from '../../../services/api/auth/responses';
import { authActions } from '../authSlice';

const schema = yup.object({
  email: yup.string().label('E-mail').required().email(),
  password: yup.string().label('Password').required(),
}).required();

function Login() {
  const [isLoading, setLoading] = useState(false);
  const { handleSubmit, formState: { errors }, register } = useForm<SignInRequest>({
    resolver: yupResolver(schema),
  });
  const dispatch = useAppDispatch();

  const onSubmit = async (body: SignInRequest) => {
    try {
      setLoading(true);
      const { data } = await api.post<LoginResponse>('auth/login', body);
      dispatch(authActions.signIn(data));
    } catch (err: any) {
      if (err.response?.data?.message) {
        const { message } = err.response.data;
        dispatch(toastActions.open({ color: 'error', message }));
        return;
      }
      dispatch(toastActions.genericError());
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login">
      <BasicCard className="login__form-card" sx={{ minWidth: 320, maxWidth: 350 }}>
        <form onSubmit={handleSubmit(onSubmit)}>
          <div className="login__form-card__group">
            <TextField
              {...register('email', { required: true })}
              error={Boolean(errors.email)}
              helperText={errors.email?.message}
              color="primary"
              label="E-mail"
            />
          </div>
          <div className="login__form-card__group">
            <TextField
              {...register('password', { required: true })}
              error={Boolean(errors.password)}
              helperText={errors.password?.message}
              color="primary"
              label="Password"
              type="password"
            />
          </div>
          <LoadingButton
            loadingPosition="center"
            loading={isLoading}
            type="submit"
            variant="contained"
          >
            Sign In
          </LoadingButton>
        </form>
      </BasicCard>
    </div>
  );
}

export { Login };
