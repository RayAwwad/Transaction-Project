
type CarouselImageProps = {
  src: string;
  
}

const Carouselimage= ({ src }:CarouselImageProps) => {
  return (
    <img className="d-block w-100" src={src} style={{ height: '100%', objectFit: 'cover' }}  />
  );
};

export default Carouselimage;
