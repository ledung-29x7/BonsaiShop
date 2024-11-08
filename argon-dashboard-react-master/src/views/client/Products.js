import FrameworkPlants from "components/Home/FrameworkPlants";
import { useLocation } from "react-router-dom";

const Products = () => {
    const location = useLocation()
    const queryPrams = new URLSearchParams(location.search)
    const search = queryPrams.get("search")
    const [searchResult,setSearchResult] = useState([])

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
        </>
    );
};
export default Products;
