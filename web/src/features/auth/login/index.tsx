import React from 'react';
import { Button, TextField } from '@mui/material';
import { BasicCard } from '../../../components/basic-card';
import './styles.css';

function Login() {
  return (
    <div className="login">
      <BasicCard className="login__form-card" sx={{ minWidth: 320, maxWidth: 350 }}>
        <div>
          <TextField color="primary" label="E-mail" />
        </div>
        <div>
          <TextField color="primary" label="Password" type="password" />
        </div>
        <Button variant="contained">Sign In</Button>
      </BasicCard>
    </div>
  );
}

export { Login };
