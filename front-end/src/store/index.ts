import {
  configureStore, ThunkAction, Action, getDefaultMiddleware,
} from '@reduxjs/toolkit';
import { authReducer } from '../features/auth/authSlice';
import { dialogReducer } from '../features/dialog/dialogSlice';
import { sidebarReducer } from '../features/sidebar/sidebarSlice';
import { ticketsReducer } from '../features/tickets/ticketsSlice';
import { toastReducer } from '../features/toast/toastSlice';

export const store = configureStore({
  reducer: {
    auth: authReducer,
    dialog: dialogReducer,
    tickets: ticketsReducer,
    toast: toastReducer,
    sidebar: sidebarReducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware({ serializableCheck: false }),
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
