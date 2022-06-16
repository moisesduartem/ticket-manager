import { Button, TextField } from '@mui/material';
import React from 'react';
import AddIcon from '@mui/icons-material/Add';
import SearchIcon from '@mui/icons-material/Search';
import { BasicCard } from '../../../../components/basic-card';
import './styles.css';
import { useAppDispatch } from '../../../../store/hooks';
import { dialogActions } from '../../../dialog/dialogSlice';
import { CreateTicketDialog } from './create-ticket-dialog';

function TicketsListFilter() {
  const dispatch = useAppDispatch();

  const onClickToAdd = () => {
    dispatch(dialogActions.open({ title: 'Create New Ticket', component: <CreateTicketDialog /> }));
  };

  return (
    <div className="tickets-list-filter">
      <BasicCard className="tickets-list-filter__container">
        <TextField className="tickets-list-filter__container__search-input" variant="outlined" />
        <div className="tickets-list-filter__container__buttons">
          <Button variant="contained">
            <SearchIcon />
          </Button>
          <Button onClick={onClickToAdd} color="primary" variant="contained">
            <AddIcon />
          </Button>
        </div>
      </BasicCard>
    </div>
  );
}

export { TicketsListFilter };
