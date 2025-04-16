import { Box, Container, createTheme, CssBaseline, ThemeProvider } from "@mui/material"
import { Outlet } from "react-router-dom"
import NavBar from "./NavBar"
import { useAppSelector } from "../store/store"

function App() {
  const { darkMode } = useAppSelector(state => state.ui);
  const paletteType = darkMode ? 'dark' : 'light';
  const theme = createTheme({
    palette: {
      mode: paletteType,
      background: {
        default: (paletteType === 'dark')
        ? 'radial-gradient(circle, #032541, #00567A, #0088B3)'
        : 'radial-gradient(circle, #00C2FF, #5AD1E8, #B3ECFF)'
      }
    }
  });
  return (
    <ThemeProvider theme={theme} >
      <CssBaseline />
      <NavBar />
      <Box sx={{
        minHeight: '100vh',
        background: darkMode
        ? 'radial-gradient(circle, #032541, #00567A, #0088B3)'
        : 'radial-gradient(circle, #00C2FF, #5AD1E8, #B3ECFF)'
      }}>
        <Container maxWidth='xl' disableGutters>
          <Outlet />
        </Container>
      </Box>
    </ThemeProvider>
  )
}

export default App
