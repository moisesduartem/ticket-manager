import React, { useState } from 'react';
import Snackbar from '@mui/material/Snackbar';
import MuiAlert, { AlertProps } from '@mui/material/Alert';
import { useAppDispatch, useAppSelector } from '../../store/hooks';
import { toastActions } from './toastSlice';

const Alert = React.forwardRef<HTMLDivElement, AlertProps>((
  props,
  ref,
) => <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />);

function Toast() {
  const settings = useAppSelector((state) => state.toast);
  const dispatch = useAppDispatch();

  const handleClose = () => {
    dispatch(toastActions.close());
  };

  return (
    <Snackbar
      open={settings.open}
      autoHideDuration={4000}
      onClose={handleClose}
      anchorOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
    >
      <Alert
        onClose={handleClose}
        severity={settings.color}
        sx={{ width: '100%' }}
      >
        {settings.message}
      </Alert>
    </Snackbar>
  );
}
export { Toast };
