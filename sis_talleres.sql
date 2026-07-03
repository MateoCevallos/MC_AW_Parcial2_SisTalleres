-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 03-07-2026 a las 21:55:52
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `sis_talleres`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inscripciones`
--

CREATE TABLE `inscripciones` (
  `inscripcion_id` int(11) NOT NULL,
  `taller_id` int(11) NOT NULL,
  `participante_id` int(11) NOT NULL,
  `fecha_inscripcion` date DEFAULT curdate(),
  `estado` enum('confirmada','cancelada') DEFAULT 'confirmada'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inscripciones`
--

INSERT INTO `inscripciones` (`inscripcion_id`, `taller_id`, `participante_id`, `fecha_inscripcion`, `estado`) VALUES
(1, 1, 1, '2026-07-01', 'confirmada'),
(2, 1, 2, '2026-07-01', 'confirmada'),
(3, 2, 3, '2026-07-02', 'cancelada'),
(4, 3, 1, '2026-07-03', 'confirmada'),
(5, 4, 5, '2026-07-04', 'confirmada');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `participantes`
--

CREATE TABLE `participantes` (
  `participante_id` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `email` varchar(150) NOT NULL,
  `telefono` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `participantes`
--

INSERT INTO `participantes` (`participante_id`, `nombre`, `apellido`, `email`, `telefono`) VALUES
(1, 'María', 'González', 'maria.gonzalez@email.com', '0991234567'),
(2, 'Carlos', 'Pérez', 'carlos.perez@email.com', '0987654321'),
(3, 'Ana', 'Rodríguez', 'ana.rodriguez@email.com', '0976543210'),
(4, 'Luis', 'Martínez', 'luis.martinez@email.com', '0965432109'),
(5, 'Sofía', 'Torres', 'sofia.torres@email.com', '0954321098');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `talleres`
--

CREATE TABLE `talleres` (
  `taller_id` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `descripcion` text DEFAULT NULL,
  `fecha` date NOT NULL,
  `ubicacion` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `talleres`
--

INSERT INTO `talleres` (`taller_id`, `nombre`, `descripcion`, `fecha`, `ubicacion`) VALUES
(1, 'Introducción a Angular', 'Fundamentos de componentes, servicios y routing', '2026-08-10', 'Aula 101, Edificio A'),
(2, 'Bases de Datos con MariaDB', 'Modelado relacional, consultas SQL y normalización', '2026-08-15', 'Laboratorio de Sistemas'),
(3, 'Desarrollo Backend con Node.js', 'Creación de APIs REST con Express', '2026-08-20', 'Aula 203, Edificio B'),
(4, 'Diseño UX/UI', 'Principios de diseño centrado en el usuario', '2026-08-25', 'Sala de Diseño'),
(5, 'Metodologías Ágiles', 'Scrum y Kanban aplicados a proyectos de software', '2026-09-01', 'Auditorio Principal'),
(6, 'Taller con el Ingeniero', 'TIC\'s', '2026-07-17', 'Laboratorio de Sistemas');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `inscripciones`
--
ALTER TABLE `inscripciones`
  ADD PRIMARY KEY (`inscripcion_id`),
  ADD UNIQUE KEY `uq_taller_participante` (`taller_id`,`participante_id`),
  ADD KEY `participante_id` (`participante_id`);

--
-- Indices de la tabla `participantes`
--
ALTER TABLE `participantes`
  ADD PRIMARY KEY (`participante_id`),
  ADD UNIQUE KEY `email` (`email`);

--
-- Indices de la tabla `talleres`
--
ALTER TABLE `talleres`
  ADD PRIMARY KEY (`taller_id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `inscripciones`
--
ALTER TABLE `inscripciones`
  MODIFY `inscripcion_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `participantes`
--
ALTER TABLE `participantes`
  MODIFY `participante_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `talleres`
--
ALTER TABLE `talleres`
  MODIFY `taller_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `inscripciones`
--
ALTER TABLE `inscripciones`
  ADD CONSTRAINT `inscripciones_ibfk_1` FOREIGN KEY (`taller_id`) REFERENCES `talleres` (`taller_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `inscripciones_ibfk_2` FOREIGN KEY (`participante_id`) REFERENCES `participantes` (`participante_id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
