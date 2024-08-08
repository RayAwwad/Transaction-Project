import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { Link, useNavigate } from "react-router-dom";
import { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import {jwtDecode} from 'jwt-decode';
import { useForm } from "react-hook-form";
import classes from "./Navigation.module.css";

type User = {
firstName: string;
lastName: string;
email: string;
balance: number;
};

const Navigation = () => {
const [show, setShow] = useState(false);
const handleClose = () => setShow(false);
const handleShow = () => setShow(true);

const navigate = useNavigate();

const [user, setUser] = useState<User | null>(null);
const [isEditing, setIsEditing] = useState(false);

const { register, handleSubmit, setValue, formState: { errors } } = useForm<User>();

useEffect(() => {
const fetchUserById = async () => {
try {
const token = localStorage.getItem("token");
if (!token) {  
throw new Error("No token found");
}

const decodedToken: any = jwtDecode(token);
const userId = decodedToken.Id;

const response = await fetch(`https://localhost:7043/api/User/${userId}`, {
method: "GET",
headers: {
"Content-Type": "application/json",
"Authorization": `Bearer ${token}`
}
});

if (!response.ok) {
throw new Error("Failed to fetch user data");
}

const userData: User = await response.json();
setUser(userData);

// Set default form values
setValue("firstName", userData.firstName);
setValue("lastName", userData.lastName);
setValue("email", userData.email);
setValue("balance", userData.balance);
} catch (error) {
console.error(error);
}
};

fetchUserById();
}, [setValue]);

const handleEdit = () => {
setIsEditing(true);
};

const handleCancel = () => {
setIsEditing(false);
};

const onSubmit = async (data: User) => {
const token = localStorage.getItem("token");
if (!token) {
console.error("No token found");
return;
}

const decodedToken: any = jwtDecode(token);
const userId = decodedToken.Id;

try {
const response = await fetch(`https://localhost:7043/api/User/${userId}`, {
method: "PUT",
headers: {
"Content-Type": "application/json",
"Authorization": `Bearer ${token}`
},
body: JSON.stringify(data)
});

if (!response.ok) {
throw new Error("Failed to update user data");
}

const updatedUser = await response.json();
setUser(updatedUser);
setIsEditing(false);
handleClose(); // Close the modal after saving
} catch (error) {
console.error(error);
}
};

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
  <Navbar.Brand as={Link} to="/landing" className={classes.swift}>Swift</Navbar.Brand>
  <div className={classes.nav}>
    <Nav className="me-auto">
      <Nav.Link className={classes.navlink} as={Link} to="/landing">Home</Nav.Link>
      <Nav.Link className={classes.navlink} as={Link} to="/transaction">Send</Nav.Link>
      <Nav.Link className={classes.navlink} onClick={handleShow}>Info</Nav.Link>
      <Nav.Link className={classes.navlink} onClick={handleLogout}>Log out</Nav.Link>
    </Nav>
    </div>
</Container>
</Navbar>

<Modal show={show} onHide={handleClose} backdrop="static" keyboard={false}>
    <Modal.Header closeButton>
    {isEditing ? (<Modal.Title>Edit info</Modal.Title>) : (<Modal.Title>Personal info</Modal.Title>)}
    </Modal.Header>
    <Modal.Body>

    <form onSubmit={handleSubmit(onSubmit)} >

    <div className={classes.inputs}>
    <label htmlFor="firstName">First Name:</label>
    <input type="text" {...register("firstName", {
       required: "First Name is required",
      pattern: {
        value: /^[a-zA-Z]{2,}$/,
        message: "Please enter a valid name"
      }
      })}
    readOnly={!isEditing} />

    </div>
    {errors.firstName && <p className={classes.error}>{errors.firstName.message}</p>}


    <div className={classes.inputs}>

    <label htmlFor="lastName">Last Name:</label>

    <input type="text" {...register("lastName", {
       required: "Last Name is required",
       pattern: {
        value: /^[a-zA-Z]{2,}$/,
        message: "Please enter a valid name"
      }
      })}
    readOnly={!isEditing} />

    </div>
    {errors.lastName && <p className={classes.error}>{errors.lastName.message}</p>}

    <div className={classes.inputs}>

    <label htmlFor="email">Email:</label>
    <input type="text" {...register("email", {
      required: "Please enter your email",
      pattern: {
        value: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,
        message: "Invalid email"
      }
    })}
    readOnly={!isEditing} />

    </div>
    {errors.email && <p className={classes.error}>{errors.email.message}</p>}


    <div className={classes.inputs}>
    <label htmlFor="balance">Balance:</label>
    <input type="number" {...register("balance", {
     required: "Please enter your balance",
      min:{
        value: 0.1,
        message: "Balance must greater than 0.1 "
      }
    })}
    readOnly={!isEditing} />
    </div>
    {errors.balance && <p className={classes.error}>{errors.balance.message}</p>}

    </form>

    <Modal.Footer>
    {isEditing ? (
    <>
      <Button onClick={handleSubmit(onSubmit)}  className={classes.save} variant="outline-success"> Save </Button>{''}
      <Button type="button" onClick={handleCancel} className={classes.cancel} variant="outline-danger">Cancel</Button>
    </>
    ) : (
    <Button type="button" onClick={handleEdit} className={classes.edit} variant="outline-secondary">Edit</Button>
    )}
    </Modal.Footer>
    </Modal.Body>
</Modal>
</>
);
}

export default Navigation;