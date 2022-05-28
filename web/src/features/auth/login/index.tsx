import React from 'react';
import { Button, TextField } from '@mui/material';
import { useForm } from 'react-hook-form';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { BasicCard } from '../../../components/basic-card';
import './styles.css';
import { SignInRequest } from './requests';

const schema = yup.object({
  email: yup.string().label('E-mail').required().email(),
  password: yup.string().label('Password').required(),
}).required();

function Login() {
  const { handleSubmit, formState: { errors }, register } = useForm<SignInRequest>({
    resolver: yupResolver(schema),
  });

  const onSubmit = (data: SignInRequest) => {
    console.log(data);
  };

  return (
    <div className="login">
      <BasicCard className="login__form-card" sx={{ minWidth: 320, maxWidth: 350 }}>
        <form onSubmit={handleSubmit(onSubmit)}>
          <div>
            <TextField
              {...register('email', { required: true })}
              error={Boolean(errors.email)}
              helperText={errors.email?.message}
              color="primary"
              label="E-mail"
            />
          </div>
          <div>
            <TextField
              {...register('password', { required: true })}
              error={Boolean(errors.password)}
              helperText={errors.password?.message}
              color="primary"
              label="Password"
              type="password"
            />
          </div>
          <Button type="submit" variant="contained">Sign In</Button>
        </form>
      </BasicCard>
    </div>
  );
}

export { Login };
