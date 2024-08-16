import { useState } from "react";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import { Link, useNavigate } from "react-router-dom";
import ModalComponent from "./ModalComponent"; 
import classes from "./Navigation.module.css";



const Navigation = () => {
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const navigate = useNavigate();

  const handleLogout = () => {
    const confirmLogout = window.confirm("Are you sure you want to log out?");

    if (confirmLogout) {
      localStorage.removeItem("token");
      navigate("/");
    }
  };

  return (
    <>
      <Navbar bg="light" data-bs-theme="light">
        <Container>
          <Navbar.Brand as={Link} to="/landing" className={classes.swift}>
            Swift
          </Navbar.Brand>
          <div className={classes.nav}>
            <Nav className="me-auto">
              <Nav.Link className={classes.navlink} as={Link} to="/landing">
                Home
              </Nav.Link>
              <Nav.Link className={classes.navlink} as={Link} to="/transaction">
                Send
              </Nav.Link>
              <Nav.Link className={classes.navlink} onClick={handleShow}>
                Info
              </Nav.Link>
              <Nav.Link className={classes.navlink} onClick={handleLogout}>
                Log out
              </Nav.Link>
            </Nav>
          </div>
        </Container>
      </Navbar>

      <ModalComponent show={show} handleClose={handleClose} />
    </>
  );
};

export default Navigation;
