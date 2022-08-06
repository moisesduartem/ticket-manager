import { createSlice } from '@reduxjs/toolkit';

export interface SidebarState {
  isOpen: boolean;
}

const initialState: SidebarState = {
  isOpen: false,
};

export const sidebarSlice = createSlice({
  name: 'sidebar',
  initialState,
  reducers: {
    toggle: (state) => ({
      ...state, isOpen: !state.isOpen,
    }),
  },
});

export const sidebarActions = sidebarSlice.actions;

export const { reducer: sidebarReducer } = sidebarSlice;
