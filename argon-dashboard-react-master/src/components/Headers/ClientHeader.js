import { Link } from "react-router-dom";
import {
    UncontrolledCollapse,
    NavbarBrand,
    Navbar,
    NavItem,
    NavLink,
    Nav,
    Container,
    Row,
    Col,
  } from "reactstrap";

const ClientHeader = () => {
  return (
    <div >
      <Navbar className="navbar-top navbar-horizontal navbar-dark bg-transparent" expand="md">
        <Container className="px-4">
          <NavbarBrand to="/" tag={Link}>
            <span className=" font-weight-bolder text-green text-xl">PLAMT</span>
          </NavbarBrand>
          <button className="navbar-toggler" id="navbar-collapse-main">
            <span className="navbar-toggler-icon" />
          </button>
          <UncontrolledCollapse navbar toggler="#navbar-collapse-main">
            <div className="navbar-collapse-header d-md-none">
              <Row>
                <Col className="collapse-brand" xs="6">
                  <Link to="/">
                    <span className=" font-weight-bolder text-lg text-green">PLAMT</span>
                  </Link>
                </Col>
                <Col className="collapse-close" xs="6">
                  <button className="navbar-toggler" id="navbar-collapse-main">
                    <span />
                    <span />
                  </button>
                </Col>
              </Row>
            </div>
            <Nav className=" ml-9" navbar>
              <NavItem>
                <NavLink className="nav-link-icon text-darker" to="/" tag={Link}>
                  <i className="ni ni-planet " />
                  <span className="nav-link-inner--text font-weight-600">Dashboard</span>
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink
                  className="nav-link-icon text-darker"
                  to="/auth/register"
                  tag={Link}
                >
                  <i className="ni ni-circle-08 " />
                  <span className="nav-link-inner--text font-weight-600">Register</span>
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink className="nav-link-icon text-darker" to="/auth/login" tag={Link}>
                  <i className="ni ni-key-25" />
                  <span className="nav-link-inner--text font-weight-600">Login</span>
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink
                  className="nav-link-icon text-darker"
                  to="/admin/user-profile"
                  tag={Link}
                >
                  <i className="ni ni-single-02" />
                  <span className="nav-link-inner--text font-weight-600">Profile</span>
                </NavLink>
              </NavItem>
            </Nav>
          </UncontrolledCollapse>
        </Container>
      </Navbar>
    </div>
  );
};

export default ClientHeader;
