<template>
    <header>
        <nav>
            
            <div class="nav-tray">
                <ul>
                    <li><router-link class="link" to="/training">Training</router-link></li>
                    <li><router-link class="link" to="/aliment">Alimentation</router-link></li>
                    
                </ul>
            </div>    
        </nav>
        
    </header>
</template>

<script>
    
    export default {
        name: "navBar",
        data() {
            return {
               
            };
        },
        computed: {
    
        },
        methods: {
         
        },
        
    };
</script>

<style lang="scss" scoped>
    header {
        position: sticky;
        width: 100%;
        z-index: 99;
        background-color: var(--primary-color);
        transition: 0.5s ease all;
        color: white;
        height: 75px;

        nav {
            display: flex;
            padding: 12px 0;
            transition: 0.5s ease all;
            width: 90%;
            margin: 0 auto;

            @media (min-width: 1140px) {
                max-width: 1140px;
            }

            ul,
            .link {
                color: white;
                font-weight: 500;
                list-style: none;
                text-decoration: none;
            }

            li {
                text-transform: uppercase;
                padding: 1rem;
                margin-left: 1rem;
            }

            .link {
                font-size: 14px;
                transition: 0.5s ease all;
                padding-bottom: 4px;
                border-bottom: 1px solid transparent;

                &:hover {
                    color: var(--accent-color-hover);
                    border-color: var(--accent-color-hover);
                }
            }

            .logo {
                display: flex;
                align-items: center;

                img {
                    width: 100px;
                    transition: 0.5s ease all;
                }
            }

            .navigation {
                flex: 1;
                display: flex;
                align-items: center;
                justify-content: flex-end;
            }

            .nav-tray {
                width: 100%;
                display: flex;
                justify-content: flex-end;
            }

            .icons {
                display: flex;
                align-items: center;
                height: 100%;

                i {
                    font-size: 24px;
                    cursor: pointer;
                    transition: 0.5s ease all;
                    padding-left: 24px;

                    &:hover {
                        color: var(--accent-color-hover);
                    }
                }
            }
        }

        .search-bar {
            background-color: var(--primary-color);
            padding: 0.7rem;
            width: 100%;

            input[type="text"] {
                margin: 0 auto;
                width: 90vw;
                padding: 0.2rem;
            }
        }

        &::after {
            content: "";
            height: 100px;
        }
    }
</style>
