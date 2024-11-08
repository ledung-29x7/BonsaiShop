import actionType from "./actionType"
import * as apis from "../../apis"

export const getHome = () => async(dispatch) => {
    try {
        const response = await apis.homePlamt();
        if(response.status === 200) {
            dispatch({
                type: actionType.GETHOME,
                homeData: response.data
            })
        }
        else{
            dispatch({
                type: actionType.GETHOME,
                homeData: null
            })
        }
    } catch (error) {
        dispatch({
            type:actionType.GETHOME,
            homeData: null
        })
    }
}

export const handleLogin = () => async(dispatch) =>{
    try {
        const response = await apis.login();
        if(response.status === 200) {
            dispatch({
                type: actionType.LOGIN,
                name: response.data
            })
        }
        else{
            dispatch({
                type: actionType.LOGIN,
                homeData: null
            })
        }
    } catch (error) {
        dispatch({
            type: actionType.LOGIN,
            homeData: null
        })
    }
}

export const inputSearch = (data) => {
    return {
        type:actionType.GET_INFO_SEARCH,
        namePlamt: data
    }
}

export const checkLogin = (ischeck) =>{
    return {
        type:actionType.CHECK_LOGIN,
        checklog: ischeck
    }
}