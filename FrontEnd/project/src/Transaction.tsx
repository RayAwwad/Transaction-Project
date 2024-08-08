import { useForm, SubmitHandler } from "react-hook-form";
import classes from "./Transaction.module.css";
import Navigation from "./Navigation";
import Button from 'react-bootstrap/Button';
import { useState, useEffect } from "react";

type Transaction = {
receiverId: number;
amount: number;
}

const Transaction = () => {

const {
register,
handleSubmit,
formState: { errors },
reset 
} = useForm<Transaction>();

const [error, setError] = useState<string | null>(null);
const [success, setSuccess] = useState<string | null>(null);


const onSubmit: SubmitHandler<Transaction> = async (data) => {

const token = localStorage.getItem("token");
try {
const response = await fetch("https://localhost:7043/api/Transaction", {
method: "POST",
headers: {
"Content-Type": "application/json",
"Authorization": `Bearer ${token}`
},
body: JSON.stringify(data)
});

if (!response.ok) {
const errorText = await response.text();
setError(errorText);
setSuccess(null);
} else {
// const transactionRecord = await response.json();
setSuccess("Transaction successful");
setError(null);
reset(); 

}
} catch (error) {
setError("An error occurred during the transaction");
setSuccess(null);
}
};

useEffect(() => {
    if (error || success) {
      const timer = setTimeout(() => {
        setError(null);
        setSuccess(null);
        
      }, 5000);

      return () => clearTimeout(timer);
    }
  }, [error, success]);

return (
    <><Navigation />
<div className={classes.body}>

<div className={classes.container}>

<form onSubmit={handleSubmit(onSubmit)}>
<h4>Transaction</h4>
<div className={classes.input}>
    <div className={classes.label}><label htmlFor="receiverId">Receiver ID: </label>
    </div>
<input type="text" {...register("receiverId",
{ required: "Receiver ID is required",
    min: {
        value: 1,
         message: "Receiver ID must be greater than 1" 
       },
    pattern: {
        value: /^\d+$/,
        message: "Please a numeric value only"
    }
 })} />
</div>
{errors.receiverId && (<p className={classes.error}>{errors.receiverId?.message}</p>)}

<div className={classes.input}>
    <div className={classes.label}>
        <label htmlFor="amount">Amount: </label>
    </div>
<input type="text" {...register("amount", { 
required: "Amount is required",
min: {
     value: 0.1,
      message: "Amount must be greater than 0.1" 
    },
pattern: {
    value: /^\d+$/,
    message: "Please a numeric value only"
}
})} />
</div>
{errors.amount && (<p className={classes.error}>{errors.amount?.message}</p>)}


{error && <p className={classes.errors}>{error}</p>}
{success && <p className={classes.success}>{success}</p>}
<Button variant="outline-dark" type="submit" className={classes.button}>
Submit
</Button>{''}
</form>



</div>
</div>
</>
);
}

export default Transaction;
