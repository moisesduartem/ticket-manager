import { createSlice } from '@reduxjs/toolkit';
import { User } from '../../domain/entities/user';
import { LocalStoragePath } from '../../infra/local-storage-path';

export interface AuthState {
    isLogged: boolean;
    user: User | null;
    token: string | null;
}

const initialState: AuthState = {
  isLogged: false,
  user: null,
  token: null,
};

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    signIn: (state, action) => ({
      ...state,
      isLogged: action.payload.user && action.payload.token,
      user: action.payload.user,
      token: action.payload.token,
    }),
  },
});

export const authActions = authSlice.actions;

export const { reducer: authReducer } = authSlice;
