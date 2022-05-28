import React from 'react';
import { Button, TextField } from '@mui/material';
import { useForm } from 'react-hook-form';
import { BasicCard } from '../../../components/basic-card';
import './styles.css';
import { SignInRequest } from './requests';

function Login() {
  const { handleSubmit, register } = useForm<SignInRequest>();

  const onSubmit = (data: SignInRequest) => {
    console.log(data);
  };

  return (
    <div className="login">
      <BasicCard className="login__form-card" sx={{ minWidth: 320, maxWidth: 350 }}>
        <form onSubmit={handleSubmit(onSubmit)}>
          <div>
            <TextField {...register('email')} color="primary" label="E-mail" />
          </div>
          <div>
            <TextField {...register('password')} color="primary" label="Password" type="password" />
          </div>
          <Button type="submit" variant="contained">Sign In</Button>
        </form>
      </BasicCard>
    </div>
  );
}

export { Login };
