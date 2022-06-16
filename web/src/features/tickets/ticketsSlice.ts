import { createSlice } from '@reduxjs/toolkit';

export interface TicketsState {
    refreshList: boolean;
}

const initialState: TicketsState = {
  refreshList: false,
};

export const ticketsSlice = createSlice({
  name: 'tickets',
  initialState,
  reducers: {
    refreshList: (state) => ({
      ...state, refreshList: true,
    }),
    finishRefresh: (state) => ({
      ...state, refreshList: false,
    }),
  },
});

export const ticketsActions = ticketsSlice.actions;

export const { reducer: ticketsReducer } = ticketsSlice;
