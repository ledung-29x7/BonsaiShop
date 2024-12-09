
const FrameworkPlants =  (props) => {
    return(
        <div className=" mt-5">
            <div className=" mb-2">
                <img width={250} height={250} src={props.img} alt="..."/>
            </div>
            <div className="d-flex flex-column align-items-center">
                <h4>{props.plamtname}</h4>
                <span>{props.price} $</span>
            </div>
        </div>
    )
}

export default FrameworkPlants