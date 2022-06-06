import { Author } from './author';
import { Category } from './category';

export interface Ticket {
    id: number;
    title: string;
    description: string;
    author: Author;
    category: Category;
    isSolved: boolean;
    createdAt: Date;
}
