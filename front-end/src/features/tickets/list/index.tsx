import React from 'react';
import { TicketsListFilter } from './filter';
import './styles.css';
import { TicketsTable } from './table';

function TicketsList() {
  return (
    <div className="tickets-list__content">
      <div className="tickets-list__content__filter">
        <TicketsListFilter />
      </div>
      <div className="tickets-list__content__table">
        <TicketsTable />
      </div>
    </div>
  );
}

export { TicketsList };
