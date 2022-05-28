import React, { useEffect, useState } from 'react';
import { Button, TextField } from '@mui/material';
import { useForm } from 'react-hook-form';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { AxiosError } from 'axios';
import { LoadingButton } from '@mui/lab';
import { BasicCard } from '../../../components/basic-card';
import './styles.css';
import { SignInRequest } from './requests';
import { api } from '../../../services/api';
import { useAppDispatch } from '../../../store/hooks';
import { toastActions } from '../../toast/toastSlice';

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
      await api.post('auth/login', body);
    } catch (err) {
      if (err instanceof AxiosError) {
        if (err.code === '401') {
          dispatch(toastActions.open({ color: 'error', message: err.message }));
        }
      }
      dispatch(toastActions.notifyGenericError());
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
