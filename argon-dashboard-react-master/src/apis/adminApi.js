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