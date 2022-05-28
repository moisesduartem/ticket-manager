import { createSlice } from '@reduxjs/toolkit';
import { User } from '../../domain/entities/user';

export interface AuthState {
    isLogged: boolean;
    user: User | null;
}

const initialState: AuthState = {
  isLogged: false,
  user: null,
};

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    signIn: (state) => ({ ...state, isLogged: Boolean(state.user), user: state.user }),
  },
});

export const { signIn } = authSlice.actions;

export const { reducer: authReducer } = authSlice;
