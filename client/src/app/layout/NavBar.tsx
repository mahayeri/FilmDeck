import { DarkMode, LightMode } from "@mui/icons-material";
import { AppBar, Box, IconButton, LinearProgress, List, ListItem, Toolbar, Typography } from "@mui/material";
import { NavLink } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../store/store";
import { setDarkMode } from "./uiSlice";

const midLinks = [
    { title: 'movie', path: '/movie' },
    { title: 'about', path: '/about' },
    { title: 'contact', path: '/contact' },
]

const rightLinks = [
    { title: 'login', path: '/login' },
    { title: 'register', path: '/register' },
]

const navStyles = {
    color: 'inherit',
    typography: 'h6',
    textDecoration: 'none',
    '&:hover': {
        color: 'grey.500'
    },
    '&.active': {
        color: '#baecf9'
    }
}
export default function NavBar() {
    const { isLoading, darkMode } = useAppSelector(state => state.ui);
    const dispatch = useAppDispatch();
    return (
        <AppBar position="sticky">
            <Toolbar sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <Box display='flex' alignItems='center'>
                    <Typography component={NavLink} to='/' variant="h6" sx={navStyles}>Film Deck</Typography>
                    <IconButton onClick={() => dispatch(setDarkMode())}>
                        {darkMode ? <DarkMode /> : <LightMode sx={{ color: 'yellow' }} />}
                    </IconButton>
                    <List sx={{ display: 'flex' }}>
                        {midLinks.map(({ title, path }) => (
                            <ListItem
                                component={NavLink}
                                to={path}
                                key={path}
                                sx={navStyles}>
                                {title.toUpperCase()}
                            </ListItem>
                        ))}
                    </List>
                </Box>
                <Box display='flex' alignItems='center'>
                    <List sx={{ display: 'flex' }}>
                        {rightLinks.map(({ title, path }) => (
                            <ListItem
                                component={NavLink}
                                to={path}
                                key={path}
                                sx={navStyles}
                            >
                                {title.toUpperCase()}
                            </ListItem>
                        ))}
                    </List>
                </Box>
            </Toolbar>
            {isLoading && (
                <Box sx={{ width: '100%' }}>
                    <LinearProgress color="secondary" />
                </Box>
            )}
        </AppBar>
    )
}