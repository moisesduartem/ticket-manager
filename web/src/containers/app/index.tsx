import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import { Toast } from '../../features/toast';
import { ApplicationRoutes } from '../../routes';
import './styles.css';

function App() {
  return (
    <>
      <Toast />
      <div id="app">
        <BrowserRouter>
          <ApplicationRoutes />
        </BrowserRouter>
      </div>
    </>
  );
}

export { App };
