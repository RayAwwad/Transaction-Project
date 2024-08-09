import classes from "./Signup.module.css";
import { SubmitHandler, useForm } from "react-hook-form";
import Button from 'react-bootstrap/Button';
import { useNavigate } from "react-router-dom";
import { FaUser } from "react-icons/fa";
import { FaLock } from "react-icons/fa";
import { IoArrowBackCircleOutline } from "react-icons/io5";
import { useState } from "react";

type Data ={
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    confirmpassword: string;
}
const Signup = () => {

const {
    register,
    handleSubmit,
    formState:{errors},
    reset,
    getValues
} = useForm<Data>();
const navigate= useNavigate();

const handleClick = ()=>{
    navigate("/");
}

const [errorMessage, setErrorMessage] = useState<string | null>(null);

const onSubmit : SubmitHandler<Data> =async (data)=>{

    try{
const response = await fetch("https://localhost:7043/api/User/signup", {
    method: "POST",
    headers: {
        "Content-Type" : "application/json",
    },
    body: JSON.stringify({
        email: data.email,
        password: data.password,
        firstname: data.firstName,
        lastname: data.lastName,
    })
})
if(!response.ok){
    
    setErrorMessage("Account already exists.");
    return;
}
const user= await response.json();
localStorage.setItem('token', user.token);
setErrorMessage(null);
navigate("/landing");
reset();
    }
    catch(error){
        setErrorMessage("Account already exists.");
    }


}


    return (
        <div className={classes.body}>
<div className={classes.container}>
  <form onSubmit={handleSubmit(onSubmit)}>
  
    <h3 className={classes.title}><IoArrowBackCircleOutline onClick={handleClick} className={classes.back}/> Sign Up </h3>
    {errorMessage && <p className={classes.error}>{errorMessage} <span className={classes.accountexists} onClick={handleClick}> Log in </span></p>}

    <div className={classes.name_info}>
         <input type="text" {...register("firstName", {
             required: "Please enter your first name",
    })}
     placeholder="First Name" />
      
         <input type="text" {...register("lastName", {
             required: "Please enter your last name",
    })} placeholder="Last Name" />
      </div>   

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

<div className={classes.login_info}>
    <input type="password" {...register("confirmpassword", {
required: "Please enter your password",
validate: (value)=> value === getValues("password") || "Password must match"
    }) } placeholder="Confirm password" />
    
</div>
<FaLock className={classes.icon}/>
{errors.confirmpassword && <p className={classes.error}>{errors.confirmpassword.message}</p>}



<Button type="submit" className={classes.button} variant="outline-light">Sign Up</Button>{' '}


  </form>
  
</div>
</div>

      );
}
 
export default Signup;