import React, { useEffect } from 'react';
import {
  Navigate,
  Route,
  Routes,
} from 'react-router-dom';
import { InternalStructure } from '../containers/internal-structure';
import { authActions } from '../features/auth/authSlice';
import { Login } from '../features/auth/login';
import { TicketsList } from '../features/tickets/list';
import { LocalStoragePath } from '../infra/local-storage-path';
import { useAppDispatch, useAppSelector } from '../store/hooks';

function ProtectedRoutes() {
  return (
    <InternalStructure>
      <Routes>
        <Route path="/" element={<TicketsList />} />
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </InternalStructure>
  );
}

function PublicRoutes() {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="*" element={<Navigate to="/login" replace />} />
    </Routes>
  );
}

function ApplicationRoutes() {
  const state = useAppSelector((state) => state.auth);
  const dispatch = useAppDispatch();

  const token = localStorage.getItem(LocalStoragePath.token);
  const user = JSON.parse(localStorage.getItem(LocalStoragePath.user) || '{}');

  useEffect(() => {
    if (!state.isLogged && Boolean(token) && Boolean(user)) {
      dispatch(authActions.signIn({ user, token }));
    }
  }, [state.isLogged]);

  return (
    state.isLogged ? <ProtectedRoutes /> : <PublicRoutes />
  );
}

export { ApplicationRoutes };
