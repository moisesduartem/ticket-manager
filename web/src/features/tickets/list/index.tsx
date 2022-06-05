import React from 'react';
import { BasicCard } from '../../../components/basic-card';
import './styles.css';
import { TicketsTable } from './table';

export function TicketsList() {
  return (
    <div className="tickets-list__content">
      <div className="tickets-list__content__table">
        <TicketsTable />
      </div>
    </div>
  );
}
