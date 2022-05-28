import { User } from '../../../domain/entities/user';

export interface LoginResponse {
    token: string;
    user: User;
}
