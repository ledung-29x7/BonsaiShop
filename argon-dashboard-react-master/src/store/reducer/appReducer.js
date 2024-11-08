import actionType from "../actions/actionType"

const initState = {
    plamtSale: [],
    plamtName: "",
    checklogin: false
}

const appReducer = (state = initState,action) =>{
    switch (action.type) {
        case actionType.GETHOME:
            return {
                ...state,
                plamtSale: action.homeData
            }
        case actionType.GET_INFO_SEARCH:
            return {
                ...state,
                plamtName: action.namePlamt
            }
        case actionType.CHECK_LOGIN:
            return {
                ...state,
                checklogin: action.checklog
            }
        default:
            return state
    }
}

export default appReducer;