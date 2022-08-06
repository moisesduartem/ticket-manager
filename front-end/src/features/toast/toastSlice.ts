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
    genericError: (state) => ({
      ...initialState,
      open: true,
    }),
    open: (state, action) => ({
      ...state, open: true, color: action.payload.color, message: action.payload.message,
    }),
    close: (state) => ({
      ...state, open: false,
    }),
  },
});

export const toastActions = toastSlice.actions;

export const { reducer: toastReducer } = toastSlice;
