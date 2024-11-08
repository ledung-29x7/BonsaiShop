
const FrameworkPlants =  (props) => {
    return(
        <div>
            <div className="">
                <img width={250} height={250} src={props.img} alt="..."/>
            </div>
            <div>
                <h4>{props.plamtname}</h4>
                <span>{props.price} $</span>
            </div>
        </div>
    )
}

export default FrameworkPlants