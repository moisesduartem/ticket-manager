import React, { useEffect, useState } from 'react';
import { yupResolver } from '@hookform/resolvers/yup';
import {
  Button,
  DialogActions,
  DialogContent,
  FormControl,
  InputLabel, MenuItem, Select, TextField,
} from '@mui/material';
import { useForm } from 'react-hook-form';
import * as yup from 'yup';
import { LoadingButton } from '@mui/lab';
import { Category } from '../../../../../domain/entities/category';
import { api } from '../../../../../services/api';
import { CreateTicketRequest } from '../../requests';
import './styles.css';
import { useAppDispatch } from '../../../../../store/hooks';
import { toastActions } from '../../../../toast/toastSlice';
import { dialogActions } from '../../../../dialog/dialogSlice';
import { ticketsActions } from '../../../ticketsSlice';

const schema = yup.object({
  title: yup.string().label('Title').required().min(12),
  description: yup.string().label('Description'),
  categoryId: yup.number().required(),
}).required();

function CreateTicketDialog() {
  const [isWaitingCreationResponse, setWaitingCreationResponse] = useState<boolean>(false);
  const [categories, setCategories] = useState<Category[]>([]);
  const { handleSubmit, formState, register } = useForm<CreateTicketRequest>({
    mode: 'onChange',
    resolver: yupResolver(schema),
    defaultValues: {
      title: '',
      description: '',
      categoryId: undefined,
    },
  });
  const dispatch = useAppDispatch();

  const fetchCategories = async () => {
    const { data } = await api.get<Category[]>('tickets/categories');
    setCategories(data);
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  const onSubmit = async (body: CreateTicketRequest) => {
    setWaitingCreationResponse(true);

    try {
      await api.post<CreateTicketRequest>('tickets', body);
      dispatch(dialogActions.close());
      dispatch(toastActions.open({ color: 'success', message: 'Ticket successfully created' }));
      dispatch(ticketsActions.refreshList());
    } catch {
      dispatch(toastActions.open({ color: 'error', message: 'Failed on attempt to create ticket' }));
    } finally {
      setWaitingCreationResponse(false);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <DialogContent dividers>
        <div className="create-ticket-dialog__input-group">
          <TextField
            {...register('title', { required: true })}
            error={Boolean(formState.errors.title)}
            helperText={formState.errors.title?.message}
            required
            color="primary"
            label="Title"
            fullWidth
          />
        </div>
        <div className="create-ticket-dialog__input-group">
          <TextField
            {...register('description')}
            error={Boolean(formState.errors.description)}
            helperText={formState.errors.description?.message}
            color="primary"
            label="Description"
            fullWidth
            multiline
            rows={4}
          />
        </div>
        <div className="create-ticket-dialog__input-group">
          <FormControl fullWidth>
            <InputLabel required id="category-select">Category</InputLabel>
            <Select
              {...register('categoryId', { required: true })}
              disabled={categories.length === 0}
              error={Boolean(formState.errors.categoryId)}
              required
              label="Category"
              labelId="category-select"
              defaultValue=""
              fullWidth
            >
              {categories.map((category) => (
                <MenuItem
                  key={category.id}
                  value={category.id}
                >
                  {category.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </div>
      </DialogContent>
      <DialogActions>
        <LoadingButton
          loading={isWaitingCreationResponse}
          type="submit"
          disabled={!formState.isValid}
          autoFocus
        >
          Create
        </LoadingButton>
      </DialogActions>
    </form>
  );
}

export { CreateTicketDialog };
