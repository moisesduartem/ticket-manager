import React, { useState } from 'react';
import Drawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import ListAltIcon from '@mui/icons-material/ListAlt';
import SettingsIcon from '@mui/icons-material/Settings';
import LogoutIcon from '@mui/icons-material/Logout';
import { Box } from '@mui/material';
import { useAppDispatch, useAppSelector } from '../../../store/hooks';
import { sidebarActions } from '../../../features/sidebar/sidebarSlice';
import { authActions } from '../../../features/auth/authSlice';

function Sidebar() {
  const { isOpen } = useAppSelector((state) => state.sidebar);
  const dispatch = useAppDispatch();
  const toggleSidebar = () => dispatch(sidebarActions.toggle());

  const onClickToSignOut = () => {
    localStorage.clear();
    dispatch(authActions.signOut());
  };

  return (
    <div>
      <Drawer
        anchor="left"
        open={isOpen}
        onClick={toggleSidebar}
        onKeyDown={toggleSidebar}
      >
        <Box
          sx={{ width: 250 }}
          role="presentation"
        >
          <List>
            <ListItem disablePadding>
              <ListItemButton>
                <ListItemIcon>
                  <ListAltIcon />
                </ListItemIcon>
                <ListItemText primary="Tickets" />
              </ListItemButton>
            </ListItem>
            <ListItem disablePadding>
              <ListItemButton>
                <ListItemIcon>
                  <SettingsIcon />
                </ListItemIcon>
                <ListItemText primary="Settings" />
              </ListItemButton>
            </ListItem>
            <ListItem disablePadding>
              <ListItemButton onClick={onClickToSignOut}>
                <ListItemIcon>
                  <LogoutIcon />
                </ListItemIcon>
                <ListItemText primary="Sign Out" />
              </ListItemButton>
            </ListItem>
          </List>
        </Box>
      </Drawer>
    </div>
  );
}

export { Sidebar };
