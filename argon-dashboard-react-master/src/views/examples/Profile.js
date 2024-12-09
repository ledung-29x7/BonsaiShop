/*!

=========================================================
* Argon Dashboard React - v1.2.4
=========================================================

* Product Page: https://www.creative-tim.com/product/argon-dashboard-react
* Copyright 2024 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/argon-dashboard-react/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/

// reactstrap components
import * as apis from "../../apis"
import {
  Button,
  Card,
  CardHeader,
  CardBody,
  FormGroup,
  Form,
  Input,
  Container,
  Row,
  Col,
} from "reactstrap";
// core components
import UserHeader from "components/Headers/UserHeader.js";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const Profile = () => {
  const [change, setChange] = useState(false)
  const navigate = useNavigate()
  const [dataChange,setDataChange] = useState({
    oldPassword: "",
    newPassword: ""
  })

  const handleChange = (e) => {
    setDataChange({...dataChange,[e.target.name]: e.target.value})
  }

  const handleSubmit = (e) => {
    e.preventDefault()
    
      const FetchApi = async() => {
        try {
          await apis.changePassword(dataChange)
          .then(res=> {
            if(res.status === 200) {
              setChange(false)
              navigate("/")
            }
          })
          
        } catch (error) {
          console.log(error)
        }
      }
      FetchApi()
    
  }
  return (
    <>
      <UserHeader />
      {/* Page content */}
      <Container className="mt--7" fluid>
        <Row>
          <Col className="order-xl-2 mb-5 mb-xl-0" xl="4">
            <Card className="card-profile shadow">
              <Row className="justify-content-center">
                <Col className="order-lg-2" lg="3">
                  <div className="card-profile-image">
                    <a href="#pablo" onClick={(e) => e.preventDefault()}>
                      <img
                        alt="..."
                        className="rounded-circle"
                        src={require("../../assets/img/theme/team-4-800x800.jpg")}
                      />
                    </a>
                  </div>
                </Col>
              </Row>
              <CardHeader className="text-center border-0 pt-8 pt-md-4 pb-0 pb-md-4">
                <div className="d-flex justify-content-between">
                  <Button
                    className="mr-4"
                    color="info"
                    href="#pablo"
                    onClick={(e) => e.preventDefault()}
                    size="sm"
                  >
                    Connect
                  </Button>
                  <Button
                    className="float-right"
                    color="default"
                    href="#pablo"
                    onClick={(e) => e.preventDefault()}
                    size="sm"
                  >
                    Message
                  </Button>
                </div>
              </CardHeader>
              <CardBody className="pt-0 pt-md-4">
                <Row>
                  <div className="col">
                    <div className="card-profile-stats d-flex justify-content-center mt-md-5">
                      <div>
                        <span className="heading">22</span>
                        <span className="description">Friends</span>
                      </div>
                      <div>
                        <span className="heading">10</span>
                        <span className="description">Photos</span>
                      </div>
                      <div>
                        <span className="heading">89</span>
                        <span className="description">Comments</span>
                      </div>
                    </div>
                  </div>
                </Row>
                <div className="text-center">
                  <h3>
                    Jessica Jones
                    <span className="font-weight-light">, 27</span>
                  </h3>
                  <div className="h5 font-weight-300">
                    <i className="ni location_pin mr-2" />
                    Bucharest, Romania
                  </div>
                  <div className="h5 mt-4">
                    <i className="ni business_briefcase-24 mr-2" />
                    Solution Manager - Creative Tim Officer
                  </div>
                  <div>
                    <i className="ni education_hat mr-2" />
                    University of Computer Science
                  </div>
                  <hr className="my-4" />
                  <p>
                    Ryan — the name taken by Melbourne-raised, Brooklyn-based
                    Nick Murphy — writes, performs and records all of his own
                    music.
                  </p>
                  <a href="#pablo" onClick={(e) => e.preventDefault()}>
                    Show more
                  </a>
                </div>
              </CardBody>
            </Card>
          </Col>
          <Col className="order-xl-1" xl="8">
            <Card className="bg-secondary shadow">
              <CardHeader className="bg-white border-0">
                <Row className="align-items-center">
                  <Col xs="8">
                    <h3 className="mb-0">My account</h3>
                  </Col>
                  <Col className="text-right" xs="4">
                    <Button
                      color="primary"
                      href="#pablo"
                      onClick={(e) => e.preventDefault()}
                      size="sm"
                    >
                      Settings
                    </Button>
                  </Col>
                </Row>
              </CardHeader>
              <CardBody>
                {change ?(
                  <Form onSubmit={handleSubmit}>
                  <h6 className="heading-small text-muted mb-4">
                    User information
                  </h6>
                  <div className="pl-lg-4">
                    <Row>
                      <Col lg="7">
                        <FormGroup>
                          <label
                            className="form-control-label"
                            htmlFor="input-first-name"
                          >
                            old Password
                          </label>
                          <Input
                            className="form-control-alternative"
                            defaultValue="Lucky"
                            id="input-first-name"
                            name="oldPassword"
                            onChange={handleChange}
                            placeholder="old Password"  
                            type="password"
                          />
                        </FormGroup>
                      </Col>
                      <Col lg="7">
                        <FormGroup>
                          <label
                            className="form-control-label"
                            htmlFor="input-last-name"
                          >
                            new Password
                          </label>
                          <Input
                            className="form-control-alternative"
                            defaultValue="Jesse"
                            id="input-last-name"
                            name="newPassword"
                            onChange={handleChange}
                            placeholder="new Password"
                            type="password"
                          />
                        </FormGroup>
                      </Col>
                    </Row>
                  </div>
                  
                  <div className="pl-lg-4">
                    <Button
                      color="primary"
                      type="submit"
                      size="md"
                    >
                      Change
                    </Button>
                  </div>
                </Form>
                ) :  (
                  <div>
                    <Button
                        color="primary"
                        type="buttom"
                        onClick={()=>setChange(true)}
                        size="md"
                      >
                        Change Password
                      </Button>
                  </div>
                )}
              </CardBody>
            </Card>
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default Profile;
