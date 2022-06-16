import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import { GenericDialog } from '../../components/dialog';
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
      <GenericDialog />
    </>
  );
}

export { App };
