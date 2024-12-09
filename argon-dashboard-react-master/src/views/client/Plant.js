import React from "react";
import { Splide, SplideSlide } from "@splidejs/react-splide";
import * as apis from "../../apis";
import { useState, useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";

const Plant = () => {
  const [plant, setPlant] = useState({});
  const navigate = useNavigate()
  const { id } = useParams();
  const customname = window.sessionStorage.getItem("name")
  const [order, setOrder] = useState([])
  // Define unique IDs for each carousel
  const mainRef = useRef(null);
  const thumbnailRef = useRef(null);

  // Options for the main carousel
  const mainOptions = {
    type: "fade",
    width: 700,
    height: 400,
    heightRatio: 0.5,
    pagination: false,
    arrows: false,
    cover: true,
  };

  // Options for the thumbnail carousel
  const thumbnailOptions = {
    fixedWidth: 100,
    fixedHeight: 60,
    gap: 10,
    rewind: true,
    pagination: false,
    isNavigation: true,
    breakpoints: {
      600: {
        fixedWidth: 60,
        fixedHeight: 44,
      },
    },
  };

  useEffect(() => {
    if (mainRef.current && thumbnailRef.current) {
      mainRef.current.sync(thumbnailRef.current.splide);
    }
  }, []);

  const handleclick =()=> {
    const FetchApi = async () => {
        try {
          await apis.order(order).then((res) => {
            if(res.status === 200){
                navigate("/")
            }
          });
        } catch (error) {
          console.log(error);
        }
      };
      FetchApi();
  }

  useEffect(() => {
    const FetchApi = async () => {
      try {
        await apis.productPlamt(id).then((res) => {
          setPlant(res);
        });
      } catch (error) {
        console.log(error);
      }
    };
    FetchApi();
  }, []);

  return (
    <div className="d-flex gap-4 pt-6">
      <div className=" w-50">
        {/* Main Carousel */}
        <Splide ref={mainRef} options={mainOptions} aria-label="Main Carousel">
          <SplideSlide>
            <img
              src="https://xanhbonsai.com/wp-content/uploads/2022/02/photo1609404615080-1609404615285490476938.jpg"
              alt="Main Image 1"
            />
          </SplideSlide>
          <SplideSlide>
            <img
              src="https://sgl.com.vn/wp-content/uploads/2021/12/top-cac-loai-cay-canh-dang-hot.jpg"
              alt="Main Image 2"
            />
          </SplideSlide>
          {/* Add more main carousel images as needed */}
        </Splide>

        {/* Thumbnail Carousel */}
        <Splide
          ref={thumbnailRef}
          options={thumbnailOptions}
          aria-label="Thumbnail Carousel"
        >
          <SplideSlide>
            <img
              src="https://xanhbonsai.com/wp-content/uploads/2022/02/photo1609404615080-1609404615285490476938.jpg"
              alt="Thumbnail Image 1"
            />
          </SplideSlide>
          <SplideSlide>
            <img
              src="https://sgl.com.vn/wp-content/uploads/2021/12/top-cac-loai-cay-canh-dang-hot.jpg"
              alt="Thumbnail Image 2"
            />
          </SplideSlide>
          {/* Add more thumbnails as needed */}
        </Splide>
      </div>
      <div className=" pt-5">
        <div className="d-flex">
            <p className=" font-weight-bold mr-2">Tên Sản Phẩm: </p>
            <span>{plant?.plantName}</span>
        </div>
        <div className="d-flex">
            <p className=" font-weight-bold mr-2">Giá: </p>
            <span>{plant?.price}</span>
        </div>
        <div className="d-flex">
            <p className=" font-weight-bold mr-2">Mô Tả: </p>
            <span>{plant?.description}</span>
        </div>
        <div>
            <button onClick={handleclick}>Đặt</button>
        </div>
       
      </div>
    </div>
  );
};
export default Plant;
