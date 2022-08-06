import axios from 'axios';
import { LocalStoragePath } from '../../infra/local-storage-path';

const api = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
});

api.interceptors.request.use((configuration) => {
  const token = localStorage.getItem(LocalStoragePath.token);

  if (token) {
    return {
      ...configuration,
      headers: {
        ...configuration.headers,
        Authorization: `Bearer ${token}`,
      },
    };
  }

  return configuration;
});

export { api };
