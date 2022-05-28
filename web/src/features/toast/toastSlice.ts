import { AlertColor } from '@mui/material';
import { createSlice } from '@reduxjs/toolkit';

export interface ToastState {
    open: boolean;
    message: string;
    color: AlertColor;
}

const initialState: ToastState = {
  open: false,
  message: 'An unexpected error has occurred.',
  color: 'error',
};

export const toastSlice = createSlice({
  name: 'toast',
  initialState,
  reducers: {
    open: (state) => ({
      ...state, open: true, color: state.color, message: state.message,
    }),
    close: (state) => ({
      ...state, open: false,
    }),
  },
});

export const { open, close } = toastSlice.actions;

export const { reducer: toastReducer } = toastSlice;
