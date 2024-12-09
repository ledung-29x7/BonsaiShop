import { Splide, SplideSlide } from "@splidejs/react-splide";
import { useEffect } from "react";
import * as actions from "../../store/actions"
import { useDispatch } from "react-redux";

const Home = () => {
  const dispatch = useDispatch()
    useEffect(()=>{
      dispatch(actions.getHome())
  },[])
  return (
    <div className="splide__track">
      <Splide className="splide__list h-100vh" aria-label="My Favorite Images">
        <SplideSlide class="splide__slide">
          <img className="h-100vh w-100" src="https://xanhbonsai.com/wp-content/uploads/2022/02/photo1609404615080-1609404615285490476938.jpg" alt="Image 1" />
          <span className="mask bg-gradient-dark opacity-3" />
        </SplideSlide>
        <SplideSlide className="splide__slide">
          <img className="h-100vh w-100" src="https://sgl.com.vn/wp-content/uploads/2021/12/top-cac-loai-cay-canh-dang-hot.jpg" alt="Image 2" />
          <span className="mask bg-gradient-dark opacity-3" />
        </SplideSlide>
      </Splide>
      <div>
        
      </div>
    </div>
  );
};
export default Home;
