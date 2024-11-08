import { useState,useEffect } from "react";
import { useSelector,useDispatch } from "react-redux";
import * as apis from "../../apis"
import * as actions from "../../store/actions"
import { Link,useNavigate,useLocation } from "react-router-dom";
import {
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
  DropdownToggle,
  UncontrolledCollapse,
  NavbarBrand,
  Navbar,
  NavItem,
  NavLink,
  Nav,
  Container,
  Form,
  FormGroup,
  InputGroupAddon,
  InputGroupText,
  Input,
  InputGroup,
  Media
} from "reactstrap";

const ClientHeader = () => {
  const dispatch = useDispatch()
  const navigate = useNavigate()
  const location = useLocation()
  const {checklogin} = useSelector(state => state.app)
  const [isChecking, setIsChecking] = useState(false);
  const [search,setSearch] = useState("")

  const handleChange = (e)=>{
    setSearch({...search, [e.target.name]: e.target.value})
  }
  const handleSubmit = (e) => {
    e.preventDefault()
    if(search.plamtName !== ''){
        dispatch(actions.inputSearch)
    }
  }

  useEffect(() => {
    checkLoggedIn();
  }, [checklogin]);

  // hủy Cookie
  function deleteCookie(name) {
    document.cookie =
      name + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
  }

  // Hàm để lấy giá trị của một cookie
  function getCookie(name) {
    const cookies = document.cookie.split("; ");
    for (let cookie of cookies) {
        const [key, value] = cookie.split("=");
        if (key === name) {
            return decodeURIComponent(value);
        }
    }
    return undefined;
}

  // check xem người dùng đã đăng nhập chưa
  function checkLoggedIn() {
    var token = getCookie("token");
    if (token) {
      // Gọi các API hoặc thực hiện các hành động khác khi người dùng đã đăng nhập
      setIsChecking(true);
    } else {
      // Hiển thị form đăng nhập hoặc các nút chức năng đăng nhập
      setIsChecking(false);
    }
  }

  const handleKeyDown = (e) =>{
    
    if(e.key === "Enter")  {
        e.preventDefault();
        if(search.trim() !== ""){
            let queryPrams = new URLSearchParams(location.search)
            queryPrams.set("search",search)
            navigate({
                pathname: "/search",
                search: queryPrams.toString()
            })
            setSearch("")
        }else{
            navigate({
                pathname: "/",
                search : ""
            })
        }
    }
  }

  // handle Logout
  const handleLogout = () => {
    
    const FetchData = async () => {
      try {
        await apis.logout().then((res) => {
          if (res.status === 200) {
            deleteCookie("token");
            checkLoggedIn();
            dispatch(actions.checkLogin(false));
            window.location.reload();
            navigate("/")
          }
        });
      } catch (error) {
        console.error(error);
      }
    };
    FetchData();
  };

  return (
    <div>
      <Navbar
        className="navbar-top navbar-horizontal navbar-dark bg-transparent"
        expand="md"
      >
        <Container className="px-4">
          <NavbarBrand to="/" tag={Link}>
            <span className=" font-weight-bolder text-green text-xl">
              PLAMT
            </span>
          </NavbarBrand>
          <button className="navbar-toggler" id="navbar-collapse-main">
            <span className="navbar-toggler-icon" />
          </button>
          <UncontrolledCollapse navbar toggler="#navbar-collapse-main">
            <Nav className=" m-auto" navbar>
              <NavItem>
                <NavLink className="nav-link-icon " to="/" tag={Link}>
                  <span className="nav-link-inner--text font-weight-600">
                    Home
                  </span>
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink
                  className="nav-link-icon"
                  to="/client/product"
                  tag={Link}
                >
                  <span className="nav-link-inner--text font-weight-600">
                    Products
                  </span>
                </NavLink>
              </NavItem>
            </Nav>
          </UncontrolledCollapse>

          <div className="d-flex ml-auto">
            <Form onSubmit={handleSubmit} className="navbar-search navbar-search-dark form-inline mr-3 d-none d-md-flex ml-lg-auto">
              <FormGroup className="mb-0">
                <InputGroup className="input-group-alternative">
                  <InputGroupAddon addonType="prepend">
                    <InputGroupText>
                      <i className="fas fa-search" />
                    </InputGroupText>
                  </InputGroupAddon>
                  <Input placeholder="Search" type="text" name="plamtName" onKeyDown={handleKeyDown} onChange={handleChange}/>
                </InputGroup>
              </FormGroup>
            </Form>

            {isChecking?(
              <div>
                <Nav className="align-items-center d-none d-md-flex" navbar>
            <UncontrolledDropdown nav>
              <DropdownToggle className="pr-0" nav>
                <Media className="align-items-center">
                  <span className="avatar avatar-sm rounded-circle">
                    <img
                      alt="..."
                      src={require("../../assets/img/theme/team-4-800x800.jpg")}
                    />
                  </span>
                  <Media className="ml-2 d-none d-lg-block">
                    <span className="mb-0 text-sm font-weight-bold">
                      Jessica Jones
                    </span>
                  </Media>
                </Media>
              </DropdownToggle>
              <DropdownMenu className="dropdown-menu-arrow" right>
                <DropdownItem className="noti-title" header tag="div">
                  <h6 className="text-overflow m-0">Welcome!</h6>
                </DropdownItem>
                <DropdownItem to="/admin/user-profile" tag={Link}>
                  <i className="ni ni-single-02" />
                  <span>My profile</span>
                </DropdownItem>
                <DropdownItem to="/admin/user-profile" tag={Link}>
                  <i className="ni ni-settings-gear-65" />
                  <span>Settings</span>
                </DropdownItem>
                <DropdownItem to="/admin/user-profile" tag={Link}>
                  <i className="ni ni-calendar-grid-58" />
                  <span>Activity</span>
                </DropdownItem>
                <DropdownItem to="/admin/user-profile" tag={Link}>
                  <i className="ni ni-support-16" />
                  <span>Support</span>
                </DropdownItem>
                <DropdownItem divider />
                <DropdownItem href="#pablo" onClick={handleLogout}>
                  <i className="ni ni-user-run" />
                  <span>Logout</span>
                </DropdownItem>
              </DropdownMenu>
            </UncontrolledDropdown>
          </Nav>
              </div>
            ):(
              <Nav className=" border rounded px-2" navbar>
                <NavItem>
                  <NavLink
                    className="nav-link-icon "
                    to="/auth/login"
                    tag={Link}
                  >
                    <span className="nav-link-inner--text py-1 font-weight-600 ">
                      Login
                    </span>
                  </NavLink>
                </NavItem>
              </Nav>)}
          </div>
        </Container>
      </Navbar>
    </div>
  );
};

export default ClientHeader;
