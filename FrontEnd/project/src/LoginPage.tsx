import { Link } from "react-router-dom";
import classes from "./LoginPage.module.css";
import { SubmitHandler, useForm } from "react-hook-form";
import Button from 'react-bootstrap/Button';
import { useNavigate } from "react-router-dom";
import { FaUser } from "react-icons/fa";
import { FaLock } from "react-icons/fa";
import { useState } from "react";


type Data ={
    email:string,
    password:string
}
const LoginPage = () => {

const {
    register,
    handleSubmit,
    formState:{errors},
    reset,
} = useForm<Data>();

const [errorMessage, setErrorMessage] = useState<string | null>(null);
const navigate = useNavigate();

const onSubmit: SubmitHandler<Data> = async (data)=>{
try{
const response = await fetch("https://localhost:7043/api/User/login",{
    method: "POST",
    headers: {
        "Content-Type" : "application/json"
    },
    body: JSON.stringify(data)
});
if (!response.ok) {
    if (response.status === 401) {
        setErrorMessage("Invalid email or password");
    } else {
        setErrorMessage("Something went wrong. Please try again.");
    }
    return;
}
const user= await response.json();
localStorage.setItem('token', user.token);

setErrorMessage(null);
navigate("/landing");
reset();
}
catch(error){
    setErrorMessage("An error occurred. Please try again later.");
}
}




    return (
        <div className={classes.body}>
            <div className={classes.container}>
  <form onSubmit={handleSubmit(onSubmit)}>

    <h3 className={classes.title}>Login</h3>

    {errorMessage && <p className={classes.error}>{errorMessage}</p>}

<div className={classes.login_info}>
    <input type="text" {...register("email", {
required: "Please enter your email",
pattern:{
    value: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,
    message: "Invalid email"
}
    }) } placeholder="Email"/>
      
</div>
<FaUser className={classes.icon} />
{errors.email && <p className={classes.error}>{errors.email.message}</p>}

<div className={classes.login_info}>
    <input type="password" {...register("password", {
required: "Please enter your password",
pattern:{
    value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$/,
    message: "Invalid password"
}
    }) } placeholder="Password" />
</div>
<FaLock className={classes.icon} />
{errors.password && <p className={classes.error}>{errors.password.message}</p>}


<Button type="submit" className={classes.button} variant="outline-light">Log In</Button>{' '}

<div className={classes.signup}>
    <span>Don't have an account? <Link to="/signup">Sign up</Link></span>
  </div>
  </form>
  
</div>
        </div>


      );
}
 
export default LoginPage;