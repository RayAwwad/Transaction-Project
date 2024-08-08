import classes from "./Landingpage.module.css";
import Navigation from "./Navigation";
import { useNavigate } from "react-router-dom";
import Carousel from 'react-bootstrap/Carousel';
import Carouselimage from "./Carouselimage";
import Button from 'react-bootstrap/Button';

const Landingpage = () => {

  const navigate = useNavigate();

  const handleClick = () => {
    navigate("/transaction");
  }

  return (
    <>
      <Navigation />
      <div className={classes.body}>
        <Carousel >
          <Carousel.Item className={classes.carousel}>
            <Carouselimage src="transaction3.jpg"/>
            <Carousel.Caption>
              <h3 className={classes.text}>Secure, reliable transfers</h3>
              <p className={classes.text}>Designed to protect your assets with precision and care.</p>
              <Button onClick={handleClick} className={classes.button} variant="outline-primary">Transfer funds</Button>{' '}
            </Carousel.Caption>
          </Carousel.Item >
          <Carousel.Item className={classes.carousel}>
            <Carouselimage src="transaction2.jfif"/>
            <Carousel.Caption>
              <h3 className={classes.text}>Fast, efficient transfers</h3>
              <p className={classes.text}>Designed to move your funds quickly and reliably</p>
              <Button onClick={handleClick} className={classes.button} variant="outline-primary">Transfer funds</Button>{' '}
            </Carousel.Caption>
          </Carousel.Item>
        </Carousel>
       
        
      </div>
    </>
  );
}

export default Landingpage;
