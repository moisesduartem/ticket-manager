import { createSlice } from '@reduxjs/toolkit';
import React from 'react';

export interface DialogState {
  isOpen: boolean;
  title: string;
  component: React.ReactNode;
}

const initialState: DialogState = {
  isOpen: false,
  title: '',
  component: React.createElement('div'),
};

export const dialogSlice = createSlice({
  name: 'dialog',
  initialState,
  reducers: {
    open: (state, { payload }) => ({
      ...state, isOpen: true, title: payload.title, component: payload.component,
    }),
    close: (state) => ({
      ...state, isOpen: false,
    }),
  },
});

export const dialogActions = dialogSlice.actions;

export const { reducer: dialogReducer } = dialogSlice;
