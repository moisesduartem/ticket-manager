import React, { useEffect, useState } from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { api } from '../../../../services/api';
import { Ticket } from '../../../../domain/entities/ticket';

function createData(
  name: string,
  calories: number,
  fat: number,
  carbs: number,
  protein: number,
) {
  return {
    name, calories, fat, carbs, protein,
  };
}

function TicketsTable() {
  const [tickets, setTickets] = useState<Ticket[]>([]);

  const fetchTickets = async () => {
    const { data } = await api.get<Ticket[]>('tickets');
    setTickets(data);
  };

  useEffect(() => {
    fetchTickets();
  }, []);

  return (
    <TableContainer component={Paper}>
      <Table aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Title</TableCell>
            <TableCell align="right">Author</TableCell>
            <TableCell align="right">Category</TableCell>
            <TableCell align="right">Solved</TableCell>
            <TableCell align="right">Creation Date</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {tickets.map((ticket) => (
            <TableRow
              key={ticket.id}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {ticket.title}
              </TableCell>
              <TableCell align="right">{ticket.author.name}</TableCell>
              <TableCell align="right">{ticket.category.name}</TableCell>
              <TableCell align="right">{ticket.isSolved ? 'Yes' : 'No'}</TableCell>
              <TableCell align="right">{new Date(ticket.createdAt).toDateString()}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}

export { TicketsTable };
