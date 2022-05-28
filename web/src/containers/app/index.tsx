import React from 'react';
import { Toast } from '../../features/toast';
import { Login } from '../../features/auth/login';
import './styles.css';

function App() {
  return (
    <>
      <Toast />
      <div id="app">
        <Login />
      </div>
    </>
  );
}

export { App };
