import FrameworkPlants from "components/Home/FrameworkPlants";
import { useLocation, useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import * as apis from "../../apis"

const Products = () => {
    const navigate = useNavigate()
    const location = useLocation()
    const queryPrams = new URLSearchParams(location.search)
    const search = queryPrams.get("search")
    const [searchResult,setSearchResult] = useState([])
    const [plant, setPlant] = useState([])
    const [image, setImage] = useState([]);
    const [imgString,setImgString] = useState("")

    useEffect(() => {
      // Hàm để hiển thị ảnh từ JSON
      function displayImages(imageDTOs) {
        setImage(`data:${imageDTOs[0]?.type};base64,${imageDTOs[0]?.image}`);
      }
      // Gọi hàm hiển thị ảnh
      displayImages(image);
    }, [image]);

    useEffect(()=>{
        if (search) {
            let result = DataSearchResult(search)
            if(result){
                setSearchResult(result)
            }
            
        }else{
            return (
                setSearchResult([]) 
            )
        }
    },[search])
    const DataSearchResult = (result) =>{
        let lowercaseResult = result.toLowerCase()
        
    }

    const FetchImage = async(id) => {
        try {
            await apis.ImagePlant(id).then(res => {
                if(res.status === 200){
                    setImage(res.data)
                }
            })
        } catch (error) {
            console.log(error)
        }
    }
    
    useEffect(()=>{
        const FetchApi = async() =>{
            try {
                await apis.homePlamt()
                .then((res)=>{
                    if(res.status === 200) {
                        setPlant(res.data);
                        FetchImage(res.plantId)
                    }
                })
            } catch (error) {
                console.log(error)
            }
        }
        FetchApi()
    },[])
    return (
        <>
            <div
                className="header pb-8 pt-5 pt-lg-8 d-flex align-items-center"
                style={{
                minHeight: "600px",
                backgroundImage:
                    "url("+ "https://freshgarden.exdomain.net/image/cache/catalog/slide/slide-img2-1360x520.jpg" + ")",
                backgroundSize: "cover",
                backgroundPosition: "center top",
                }}
            >
                <span className="mask bg-gradient-dark opacity-3" />
            </div>
                
            <div className="container">
                <div className="row">
                    {searchResult.length>0 ? (
                        searchResult.map(res=>
                            <div>
                                    <div></div>
                                <div className="grid">
                                    <FrameworkPlants 
                                        img={"https://cayxanhminhhieu.com/wp-content/uploads/2022/06/ca-hang-sen-da.jpg"}
                                        plamtname="hoa đá"
                                        price= "12"
                                        />
                                </div>
                            </div>
                        )
                    ) : (
                        plant.map(res => 
                            <div className="col-3" onClick={()=> { navigate(`/client/plant/${res.id}`)}}>
                                    
                                <FrameworkPlants 
                                    img={"https://cayxanhminhhieu.com/wp-content/uploads/2022/06/ca-hang-sen-da.jpg"}
                                    plamtname={res.plantName}
                                    price= {res.price}
                                    />
                            </div>
                        )
                    )}
                </div>
            </div>
        </>
    );
};
export default Products;
