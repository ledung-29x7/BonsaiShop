import { BrowserRouter, Route, Routes, Navigate } from "react-router-dom";
import "assets/plugins/nucleo/css/nucleo.css";
import "@fortawesome/fontawesome-free/css/all.min.css";
import "assets/scss/argon-dashboard-react.scss";

import AdminLayout from "layouts/Admin.js";
import AuthLayout from "layouts/Auth.js";
import Client from "layouts/Client";

const App = () => {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/admin/*" element={<AdminLayout />} />
          <Route path="/auth/*" element={<AuthLayout />} />
          <Route path="/client/*" element={<Client />}/>
          <Route path="*" element={<Navigate to="/client/home" replace />} />
        </Routes>
      </BrowserRouter>
    </>
  );
};

export default App;