import React from 'react';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import { IconButton } from '@mui/material';
import Button from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import CloseIcon from '@mui/icons-material/Close';
import { useAppDispatch, useAppSelector } from '../../store/hooks';
import { dialogActions } from '../../features/dialog/dialogSlice';

const GenericDialogBootstrap = styled(Dialog)(({ theme }) => ({
  '& .MuiDialogContent-root': {
    padding: theme.spacing(2),
  },
  '& .MuiDialogActions-root': {
    padding: theme.spacing(1),
  },
}));

export interface DialogTitleProps {
  children?: React.ReactNode;
  onClose: () => void;
}

function GenericDialogTitle(props: DialogTitleProps) {
  const { children, onClose, ...other } = props;

  return (
    <DialogTitle sx={{ m: 0, p: 2 }} {...other}>
      {children}
      {onClose ? (
        <IconButton
          aria-label="close"
          onClick={onClose}
          sx={{
            position: 'absolute',
            right: 8,
            top: 8,
            color: (theme) => theme.palette.grey[500],
          }}
        >
          <CloseIcon />
        </IconButton>
      ) : null}
    </DialogTitle>
  );
}

function GenericDialog() {
  const { component: dialogComponent, title: dialogTitle, isOpen } = useAppSelector(
    (state) => state.dialog,
  );

  const dispatch = useAppDispatch();

  const handleClose = () => {
    dispatch(dialogActions.close());
  };

  return (
    <GenericDialogBootstrap
      fullWidth
      onClose={handleClose}
      open={isOpen}
    >
      <GenericDialogTitle onClose={handleClose}>{dialogTitle}</GenericDialogTitle>
      {dialogComponent}
    </GenericDialogBootstrap>
  );
}

export { GenericDialog };
