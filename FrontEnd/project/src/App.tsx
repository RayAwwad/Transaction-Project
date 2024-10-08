import LoginPage from "./Components/LoginPage";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Signup from "./Components/Signup";
import Landingpage from "./Components/Landingpage";

import Transaction from "./Components/Transaction";


function App() {
  return (
    <Router>
       <div className="App">
        <Routes>
          <Route path="/" element={<LoginPage />} />
          <Route path="/signup" element={<Signup />} />
          <Route path="/landing" element={<Landingpage />} />
          
          <Route path="/transaction" element={<Transaction />} />
        </Routes>
 
   
    </div>
    </Router>
   
  );
}

export default App;

// String done
// number done
// array done
// object done
// array of objects done
// union done
// cssproperties done
// handleclick done
// handlechange done
// React.ReactNode done (to check again)
// React.ReactElement (to check)