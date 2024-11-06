import { Splide, SplideSlide } from "@splidejs/react-splide";

const Home = () => {
  return (
    <div className="splide__track">
      <Splide className="splide__list h-100vh" aria-label="My Favorite Images">
        <SplideSlide class="splide__slide">
          <img className="h-100vh w-100" src="https://xanhbonsai.com/wp-content/uploads/2022/02/photo1609404615080-1609404615285490476938.jpg" alt="Image 1" />
        </SplideSlide>
        <SplideSlide className="splide__slide">
          <img className="h-100vh w-100" src="https://sgl.com.vn/wp-content/uploads/2021/12/top-cac-loai-cay-canh-dang-hot.jpg" alt="Image 2" />
        </SplideSlide>
      </Splide>
    </div>
  );
};
export default Home;
