import * as React from 'react';
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import Button from '@mui/material/Button';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import ListAltIcon from '@mui/icons-material/ListAlt';
import SettingsIcon from '@mui/icons-material/Settings';
import LogoutIcon from '@mui/icons-material/Logout';

function Sidebar() {
  const [isOpen, setOpen] = React.useState(false);

  const toggleDrawer = () => setOpen(!isOpen);

  return (
    <div>
      <Button onClick={toggleDrawer}>left</Button>
      <Drawer
        anchor="left"
        open={isOpen}
        onClose={toggleDrawer}
      >
        <Box
          sx={{ width: 250 }}
          role="presentation"
          onClick={toggleDrawer}
          onKeyDown={toggleDrawer}
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
              <ListItemButton>
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
