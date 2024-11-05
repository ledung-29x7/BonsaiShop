import actionType from "store/actions/actionType"

const initState = {
    test: []
}

const appReducer = (state = initState,action) =>{
    switch (action.type) {
        case actionType.LOGIN:
            return state
    
        default:
            return state
    }
}

export default appReducer;