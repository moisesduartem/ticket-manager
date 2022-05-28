import * as React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import { SxProps, Theme } from '@mui/material';

export interface BasicCardProps {
  children?: React.ReactNode;
  className?: string;
  sx?: SxProps<Theme>;
}

function BasicCard({ children, className, sx = { minWidth: 275 } }: BasicCardProps) {
  return (
    <Card sx={sx}>
      <CardContent className={className}>{children}</CardContent>
    </Card>
  );
}

export { BasicCard };