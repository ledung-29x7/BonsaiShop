import React from "react";
import { useLocation, Route, Routes, Navigate } from "react-router-dom";
// reactstrap components
import { Container, Row, Col } from "reactstrap";

// core components
import AuthFooter from "components/Footers/AuthFooter.js";
import ClientHeader from "components/Headers/ClientHeader";

import routes from "routes.js";

const Client = () => {
    const mainContent = React.useRef(null);

    const getRoutes = (routes) => {
        return routes.map((prop, key) => {
          if (prop.layout === "/client") {
            return (
              <Route path={prop.path} element={prop.component} key={key} exact />
            );
          } else {
            return null;
          }
        });
    };

    return (
        <>
        <div className="main-content" ref={mainContent}>
            <ClientHeader
            
            />
            <Routes>
            {getRoutes(routes)}
            <Route path="*" element={<Navigate to="/client/home" replace />} />
            </Routes>
            <Container fluid>
            <AuthFooter />
            </Container>
        </div>
        </>
    );
};

export default Client;
