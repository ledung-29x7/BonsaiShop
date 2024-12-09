import axios from "../axios";

export const login = (post) => new Promise(async (resolve, reject) => {
  try {
    const response = await axios.post("/Auth/login", post, {
      withCredentials: true,
    });
    resolve(response);
  } catch (error) {
    reject(error);
  }
});

export const register = (param) => new Promise(async (resolve, reject) => {
    try {
      const response = await axios.post("/Auth/register",param,{
        withCredentials: true
      });
      resolve(response)
    } catch (error) {
      reject(error)
    }
  })

  export const logout = () =>
    new Promise(async (resolve, reject) => {
      try {
        const response = await axios({
          url: `/Auth/logout`,
          method: "post",
          withCredentials: true,
          headers: {
            Authorization: `Bearer ${window.sessionStorage.getItem("token")}`,
          },
        });
        resolve(response);
      } catch (error) {
        reject(error);
      }
    });

export const changePassword = (param) => new Promise(async (resolve, reject) => {
  try {
    const response = await axios({
      url:"/Auth/change-password",
      method:"Post",
      data: param,
      withCredentials: true,
      headers: {
        Authorization: `Bearer ${window.sessionStorage.getItem("token")}`
      }
    })
    resolve(response)
  } catch (error) {
    reject(error)
  } 
})